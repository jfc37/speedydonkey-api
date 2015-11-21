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

@block @update @golden_path
Scenario: Update block to invite only
	Given a block exists
	And the block needs to change to invite only
	When the block is updated
	And blocks for enrolment is requested
	Then the request returns not found
	And the block is invite only

@block @update @golden_path
Scenario: Update invite only block to public
	Given an invite only block exists
	And the block needs to change from invite only
	When the block is updated
	And blocks for enrolment is requested
	Then the block is not invite only
	And there are blocks available for enrolment

