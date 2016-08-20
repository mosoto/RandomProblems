package globberSA

type state int

type StateSet struct {
	set map[state]bool
}

func NewStateSet() *StateSet {
	return &StateSet{
		set: make(map[state]bool),
	}
}

func (stateSet *StateSet) Add(s state) bool {
	_, found := stateSet.set[s]
	stateSet.set[s] = true
	return !found
}

func (stateSet *StateSet) AddRange(s []state) {
	for _, i := range s {
		stateSet.Add(i)
	}
}

func (stateSet *StateSet) Items() []state {
	keys := make([]state, 0, len(stateSet.set))
	for k := range stateSet.set {
		keys = append(keys, k)
	}

	return keys
}

func (stateSet *StateSet) Delete(s state) {
	delete(stateSet.set, s)
}

func (stateSet *StateSet) Contains(s state) bool {
	_, found := stateSet.set[s]
	return found
}
