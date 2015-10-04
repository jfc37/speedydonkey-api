Feature: UpdateBlock

@block @update @golden_path
Scenario: Update the day a block is on
	Given a block exists
	And the day the block is on needs to change
	When the block is updated
	Then the blocks start and end time is updated
	And the block's classes start and end time is updated

@block @update @golden_path
Scenario: Update the day a block is on flows on when next block is generated
	Given a block exists
	And the day the block is on has been updated
	When the next block is generated
	Then the first class of the second block is a week after the last class of the first block
