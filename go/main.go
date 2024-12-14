package main

import "fmt"

type Obj struct {
    Name string
    Age int
}

func Add(a, b int) int {
    return a + b
}

func GenObjArr(n int) []Obj {
    result :=  make([]Obj, n)

    for i := 0; i < n; i++ {
        obj := Obj {
            Name: fmt.Sprintf("Name_%d", i),
            Age: i + 1,
        }
        result[i] = obj
    }

    return result
} 


func main() {
   fmt.Println("Start") 

   result := Add(2, 3) 
   fmt.Printf("Add operation result: %d \n", result)

   objs := GenObjArr(result)
   fmt.Println("Rand objs:", objs)

   fmt.Println("Stop")
}
