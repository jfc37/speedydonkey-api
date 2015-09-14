Feature: UpdateClass

@class @update @golden_path
Scenario: Update class time
	Given a block exists
	And a class time needs to change
	When the class is updated
	Then the class's start and end time is updated
	And the blocks start and end time is unchanged
	And the levels start and end time is unchanged