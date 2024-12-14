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

### 2. Go install (optional)

1. install `richgo`
   NOTE: if you want to use pretty test output. You can install it. (setupping below)
   - installation:
     - `go install github.com/kyoh86/richgo@latest`
   - check:
     - `richgo version`

### 3. Setup vim-test

Add in your config file `vim-test.lua` next:

1. tool output:

```lua
-- Use test output via richgo in Go.
`vim.g["test#go#runner"] = "richgo"`
-- If you want to use default go test without richgo. Set:
--    - `vim.g["test#go#runner"] = "go"`

vim.g["test#strategy"] = "neovim" -- run in dynamic window neovim

```

2. verbose (optional)
   NOTE: By default if your test has failed results. You will see only failed test. If you want to see all, run: - `:TestFile -v`

- if you want to see always all tests, add to file:

```lua

    vim.g["test#go#options"] = "-v"
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
   ![vim_test](img/vim_test.png)

# Debugger (dap, dapui)

### 1. Go install

1. install Delve (debugger for go)
   - `go install github.com/go-delve/delve/cmd/dlv@latest`

### 2. Nvim plugins

1. go to your `packer.lua`
2. add next:

```lua
  use {
  "mfussenegger/nvim-dap",              -- main plugind for debug
  "rcarriga/nvim-dap-ui",               -- UI for nvim-dap
  "nvim-telescope/telescope-dap.nvim",  -- integration nvim-dap with Telescope
  "theHamsta/nvim-dap-virtual-text",    -- virtual inline text

  -- GO
  "leoluz/nvim-dap-go",                 -- Go-adapter for nvim-dap
}
```

3. run `:PackerSync`

### 3. Setup nvim-dap

1. create lua file, ex: `dap.lua`
2. add next:

```lua
local dap = require("dap")
local dapgo = require("dap-go")

-- ###### setup Go
dapgo.setup() -- auto setup debugger Delve

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

1. go to main.go
2. select any line or lines
3. set breakpoints `<leader>B`
4. run debug `<F5>`
5. check result
   ![dap_ui](img/dap_ui.png)
