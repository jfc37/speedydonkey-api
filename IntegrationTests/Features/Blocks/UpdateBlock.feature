Feature: UpdateBlock

@block @update @golden_path
Scenario: Update the day a block is on
	Given a block exists
	And the day the block is on needs to change
	When the block is updated
	And the blocks start and end time is updated
	Then the block's classes start and end time is updated
	And the levels start and end time is unchanged