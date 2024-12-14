package main

import (
	"testing"
)

func TestAdd_Success(t *testing.T) {
    actual := Add(2, 3)
    expected := 5
    
    if actual != expected {
        t.Errorf(
            "Add(2, 3) = %d; expected %d", actual, expected,
        )
    }
}

func TestAdd_Fail(t *testing.T) {
    actual := Add(2, 3)
    expected := 10
    
    if actual != expected {
        t.Errorf(
            "Add(2, 3) = %d; expected %d", actual, expected,
        )
    }
}
