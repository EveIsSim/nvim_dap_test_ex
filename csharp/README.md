> ⚠️ NOTE:
    - I will write step by step and some headings can have same or similar naming.
    - I use NVIM + packer manager. So setup will be described according to these tools.

# Tests

### 1. Nvim plugins

1. go to your `packer.lua`
2. add next:

```lua
use {
  "vim-test/vim-test"
}
```

3. run `:PackerSync`


### 2. Setup vim-test

Add in your config file `vim-test.lua` next:

1. tool output:

```lua

-- ###### setup Csharp
vim.g["test#runner_commands"] = { cs = "dotnet test" }
-- ###### setup Csharp

vim.g["test#strategy"] = "neovim" -- run in dynamic window neovim

```

### 3. Mapping/KeyBinding

Add in your lua config next:

```lua
vim.api.nvim_set_keymap("n", "<leader>tn", ":TestNearest<CR>", { noremap = true, silent = true })   -- ex: test by cursor
vim.api.nvim_set_keymap("n", "<leader>tf", ":TestFile<CR>", { noremap = true, silent = true })      -- ex: all tests in file
vim.api.nvim_set_keymap("n", "<leader>ts", ":TestSuite<CR>", { noremap = true, silent = true })     -- ex: all tests in project
vim.api.nvim_set_keymap("n", "<leader>tl", ":TestLast<CR>", { noremap = true, silent = true })      -- ex: last test
```

### 4. Finish ✅

1. go to test file
2. tap `<leader>ts`
3. check result
   ![vim_test](img/vim_test_sharp2.png)

# Debugger (dap, dapui)

### 1. Csharp install

1. Install `netcoredbg` - (debugger for .NET)
```sh
1. git clone https://github.com/Samsung/netcoredbg
2. cd netcoredbg 
3. build from source, read README.md `Compiling` block
3. add to $PATH
    - `export PATH=$PATH:/usr/local/netcoredbg`
```

2. Test, that your project will support testing:
```sh
dotnet new xunit -o TestProject
cd TestProject
dotnet test
```

### 2. Nvim plugins

1. go to your `packer.lua`
2. add next:
```lua
  use {
  "mfussenegger/nvim-dap",              -- main plugind for debug
  "rcarriga/nvim-dap-ui",               -- UI for nvim-dap
  "nvim-telescope/telescope-dap.nvim",  -- integration nvim-dap with Telescope
  "theHamsta/nvim-dap-virtual-text",    -- virtual inline text
}
```
3. run `:PackerSync`

### 3. Setup nvim-dap

1. create lua file, ex: `dap.lua`
2. add next:

```lua
local dap = require("dap")
local dapgo = require("dap-go")

dap.adapters.coreclr = {
  type = "executable",
  command = "/usr/local/netcoredbg",
  args = { "--interpreter=vscode" }
}

dap.configurations.cs = {
    {
        type = "coreclr",
        name = "Launch - NetCoreDbg",
        request = "launch",
        program = function()
            -- recurcive searching all DLLs
            local dlls = vim.fn.glob(vim.fn.getcwd() .. "/**/bin/Debug/net*/*.dll", 0, 1)

            if #dlls == 0 then
                vim.notify("No DLLs found in the project.", vim.log.levels.ERROR)
                return nil
            elseif #dlls == 1 then
                vim.notify("Selected DLL: " .. dlls[1], vim.log.levels.INFO)
                return dlls[1]
            else
                local dll_list = table.concat(dlls, "\n")
                local choice = vim.fn.inputlist({"Select DLL:", dll_list})
                if choice < 1 or choice > #dlls then
                    vim.notify("Invalid choice. Canceling debug.", vim.log.levels.ERROR)
                    return nil
                end
                return dlls[choice]
            end
        end,
    },
}

-- ###### setup dapui
local dapui = require("dapui")
dapui.setup()

-- open/close UI when start/stop debugging
dap.listeners.after.event_initialized["dapui_config"] = function()
  dapui.open()
end
dap.listeners.before.event_terminated["dapui_config"] = function()
  dapui.close()
end
dap.listeners.before.event_exited["dapui_config"] = function()
  dapui.close()
end

require("nvim-dap-virtual-text").setup()
require("telescope").load_extension("dap")

-- ###### setup dapui

```

### 3. Mapping/KeyBinding

Add in your lua config next:

```lua
vim.api.nvim_set_keymap("n", "<F5>", ":lua require'dap'.continue()<CR>", { noremap = true, silent = true })
vim.api.nvim_set_keymap("n", "<F10>", ":lua require'dap'.step_over()<CR>", { noremap = true, silent = true })
vim.api.nvim_set_keymap("n", "<F11>", ":lua require'dap'.step_into()<CR>", { noremap = true, silent = true })
vim.api.nvim_set_keymap("n", "<F12>", ":lua require'dap'.step_out()<CR>", { noremap = true, silent = true })
vim.api.nvim_set_keymap("n", "<Leader>b", ":lua require'dap'.toggle_breakpoint()<CR>", { noremap = true, silent = true })
vim.api.nvim_set_keymap("n", "<Leader>B", ":lua require'dap'.set_breakpoint(vim.fn.input('Breakpoint condition: '))<CR>", { noremap = true, silent = true })
vim.api.nvim_set_keymap("n", "<Leader>dr", ":lua require'dap'.repl.open()<CR>", { noremap = true, silent = true })
vim.api.nvim_set_keymap("n", "<Leader>dl", ":lua require'dap'.run_last()<CR>", { noremap = true, silent = true })

```

### 4. Finish ✅

1. go to Program.cs
2. select any line or lines
3. set breakpoints `<leader>B`
4. run debug `<F5>`
5. check result
   ![dap_ui](img/dap_ui_csharp.png)
