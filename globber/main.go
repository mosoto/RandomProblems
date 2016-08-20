package main

import (
	"fmt"

	"github.com/mosoto/RandomProblems/globber/globberSA"
)

type Test struct {
	Glob     string
	String   string
	Expected bool
}

func main() {
	runTest(Test{Glob: "", String: "", Expected: true})
	runTest(Test{Glob: "", String: "a", Expected: false})
	runTest(Test{Glob: "a", String: "", Expected: false})
	runTest(Test{Glob: "abc", String: "abc", Expected: true})
	runTest(Test{Glob: "a*", String: "abc", Expected: true})
	runTest(Test{Glob: "a??", String: "abc", Expected: true})
	runTest(Test{Glob: "a??", String: "a", Expected: false})
	runTest(Test{Glob: "a??", String: "", Expected: false})
	runTest(Test{Glob: "*", String: "", Expected: true})
	runTest(Test{Glob: "*", String: "foobar", Expected: true})
	runTest(Test{Glob: "**", String: "foobar", Expected: true})
	runTest(Test{Glob: "*o*", String: "foobar", Expected: true})
	runTest(Test{Glob: "*o*", String: "bar", Expected: false})
	runTest(Test{Glob: "*o*??", String: "wow42", Expected: true})
	runTest(Test{Glob: "*o*??", String: "wow", Expected: false})
	runTest(Test{Glob: "*o*?*", String: "wow", Expected: true})
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
	globber := globberSA.NewGlobber(glob)
	return globber.Matches(str)
}
