package globberSA

type Globber interface {
	Matches(str string) bool
}

type transitionTable map[state]map[rune][]state

const anyChar = '\x00'

type globberSA struct {
	table      transitionTable
	startState state
	endState   state
}

func NewGlobber(regex string) Globber {
	table, startState, endState := createTransitionTable(regex)

	return &globberSA{
		table:      table,
		startState: startState,
		endState:   endState,
	}
}

func createTransitionTable(regex string) (table transitionTable, startState state, endState state) {
	startState = state(0)
	table = make(transitionTable)
	currentState := startState

	for _, char := range regex {
		if table[currentState] == nil {
			table[currentState] = make(map[rune][]state)
		}

		var nextState state
		switch char {
		case '*':
			nextState = currentState
			table[currentState][anyChar] = append(table[currentState][anyChar], currentState)
		case '?':
			nextState = state(currentState + 1)
			table[currentState][anyChar] = append(table[currentState][anyChar], nextState)
		default:
			nextState = state(currentState + 1)
			table[currentState][char] = append(table[currentState][char], nextState)
		}

		currentState = nextState
	}

	endState = currentState

	if table[endState] == nil {
		table[endState] = make(map[rune][]state)
	}

	return
}

func (globber *globberSA) Matches(str string) bool {
	states := NewStateSet()
	states.Add(globber.startState)

	for _, char := range str {
		nextStates := NewStateSet()
		for _, state := range states.Items() {
			ns, found := globber.table[state][char]

			if found {
				nextStates.AddRange(ns)
			}

			ns, found = globber.table[state]['\x00']
			if found {
				nextStates.AddRange(ns)
			}
		}
		states = nextStates
	}

	return states.Contains(globber.endState)
}
