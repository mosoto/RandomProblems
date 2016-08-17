package main

type Globber interface {
	Matches(str string) bool
}

type globberSA struct {
}

func NewGlobber(regex string) Globber {
	return &globberSA{}
}

func (globber *globberSA) Matches(str string) bool {

}
