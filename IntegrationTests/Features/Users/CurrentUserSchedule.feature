Feature: CurrentUserSchedule

@user @currentuser @schedule @get
Scenario: Get current user's schedule when enrolled
	Given a block exists
	And the current user enrols in the block
	When the current user schedule is retrieved
	Then the current user's schedule is not emtpy

@user @currentuser @schedule @get
Scenario: Get current user schedule when not enrolled in anything
	Given the current user isn't enrolled in any blocks
	When the current user schedule is retrieved
	Then the current user's schedule is emtpy