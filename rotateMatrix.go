package main

import (
	"fmt"
)

type coord struct {
	x int
	y int
}

type matrixRing struct {
	matrix         [][]int
	upperLeftCoord coord
	length         int
	height         int
}

func (mr *matrixRing) ringLength() int {
	if mr.height == 1 {
		return mr.length
	} else if mr.length == 1 {
		return mr.height
	} else {
		return 2*mr.length + 2*(mr.height-2)
	}
}

func (mr *matrixRing) shiftRight(n int) {
	rotationN := n % mr.ringLength()

	mr.reverse(0, mr.ringLength()-1)
	mr.reverse(0, rotationN-1)
	mr.reverse(rotationN, mr.ringLength()-1)
}

func (mr *matrixRing) reverse(start int, end int) {
	i := start
	j := end
	for i < j {
		mr.swap(i, j)

		i++
		j--
	}
}

func (mr *matrixRing) swap(indexA int, indexB int) {
	coordA := mr.indexToCoord(indexA)
	coordB := mr.indexToCoord(indexB)

	temp := mr.matrix[coordA.y][coordA.x]
	mr.matrix[coordA.y][coordA.x] = mr.matrix[coordB.y][coordB.x]
	mr.matrix[coordB.y][coordB.x] = temp
}

func (mr *matrixRing) indexToCoord(index int) coord {
	upperRightIndex := mr.length - 1
	bottomRightIndex := upperRightIndex + mr.height - 1
	bottomLeftIndex := bottomRightIndex + mr.length - 1

	if index <= upperRightIndex {
		return coord{x: mr.upperLeftCoord.x + index, y: mr.upperLeftCoord.y}
	}

	if index <= bottomRightIndex {
		return coord{x: mr.upperLeftCoord.x + mr.length - 1, y: mr.upperLeftCoord.y + index - upperRightIndex}
	}

	if index <= bottomLeftIndex {
		bottomRightX := mr.upperLeftCoord.x + mr.length - 1
		return coord{x: bottomRightX - (index - bottomRightIndex), y: mr.upperLeftCoord.y + mr.height - 1}
	}

	bottomLeftY := mr.upperLeftCoord.y + mr.height - 1
	return coord{x: mr.upperLeftCoord.x, y: bottomLeftY - (index - bottomLeftIndex)}
}

func main() {
	matrix1 := [][]int{{1, 2, 3, 4, 5}}
	testRotation(matrix1, 3)

	matrix2 := [][]int{
		{1},
		{2},
		{3},
		{4},
	}
	testRotation(matrix2, 3)

	matrix3 := [][]int{
		{1, 2},
		{3, 4},
		{5, 6},
	}
	testRotation(matrix3, 3)

	matrix4 := [][]int{
		{1, 2, 3},
		{4, 5, 6},
	}
	testRotation(matrix4, 3)

	matrix5 := [][]int{
		{0, 1, 2, 3},
		{4, 5, 6, 7},
		{8, 9, 0, 1},
	}
	testRotation(matrix5, 3)
}

func testRotation(matrix [][]int, n int) {
	printMatrix(matrix)
	rotateMatrix(matrix, n)

	fmt.Print("-----------------------\n")
	printMatrix(matrix)
	fmt.Println()
}

func printMatrix(matrix [][]int) {
	for _, row := range matrix {
		fmt.Printf("%v\n", row)
	}
}

func rotateMatrix(matrix [][]int, n int) {
	ringHeight := len(matrix)
	ringLength := len(matrix[0])
	numRings := (min(ringHeight, ringLength) - 1) / 2

	for i := 0; i <= numRings; i++ {
		ring := matrixRing{
			matrix:         matrix,
			upperLeftCoord: coord{x: i, y: i},
			length:         ringLength,
			height:         ringHeight,
		}

		ring.shiftRight(n)

		ringHeight -= 2
		ringLength -= 2
	}
}

func min(a, b int) int {
	if a < b {
		return a
	}
	return b
}
