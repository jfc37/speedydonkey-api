Feature: CreateBlock

@block @create @golden_path
Scenario: Generate first block
	Given a valid block is ready to be submitted
	When the block is attempted to be created
	Then block can be retrieved
	And the blocks dates are in utc
	And classes are created for the block
	And the correct number of classes are created

@block @create @golden_path
Scenario: Generate second block
	Given a block exists
	When the next block is generated
	Then block can be retrieved
	And the first class of the second block is a week after the last class of the first block