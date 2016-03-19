Feature: UpdatePass

@pass @update
Scenario: Update pass
	Given a user exists
	And the user has a valid clip pass
	And the pass needs to be changed
	When the pass is updated
	Then the request is successful
	And the pass details are updated
