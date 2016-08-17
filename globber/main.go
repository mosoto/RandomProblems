package main

import (
	"fmt"

	"github.com/RandomProblems/globber"
)

type Test struct {
	Glob     string
	String   string
	Expected bool
}

func main() {
	runTest(Test{Glob: "", String: "", Expected: true})
	runTest(Test{Glob: "a", String: "", Expected: false})
	runTest(Test{Glob: "abc", String: "abc", Expected: true})
	runTest(Test{Glob: "a*", String: "abc", Expected: true})
	runTest(Test{Glob: "a??", String: "abc", Expected: true})
	runTest(Test{Glob: "a??", String: "a", Expected: true})
	runTest(Test{Glob: "a??", String: "", Expected: false})
	runTest(Test{Glob: "*", String: "", Expected: true})
	runTest(Test{Glob: "*", String: "foobar", Expected: true})
	runTest(Test{Glob: "**", String: "foobar", Expected: true})
	runTest(Test{Glob: "*o*", String: "foobar", Expected: true})
	runTest(Test{Glob: "*o*", String: "bar", Expected: false})
}

func runTest(testcase Test) {
	matches := match(testcase.Glob, testcase.String)

	passStr := "PASSED"
	if matches != testcase.Expected {
		passStr = "FAILED"
	}

	fmt.Printf("%s - %+v\n", passStr, testcase)
}

func match(glob string, str string) bool {
	globber := NewGlobber(glob)
	return globber.Matches(str)
}
