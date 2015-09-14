Feature: CreateBlock

@block @create @golden_path
Scenario: Generate first block
	Given a level exists
	When a block is generated from the level
	Then block can be retrieved

@block @create @golden_path
Scenario: Generate second block
	Given a block exists
	When the next block is generated
	Then block can be retrieved
	And the first class of the second block is a week after the last class of the first block