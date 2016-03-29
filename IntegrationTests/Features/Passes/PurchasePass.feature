Feature: PurchasePass

@pass @create
Scenario: User purchases clip pass from a teacher
	Given a user exists
	And a pass template exists
	When the user purchases a pass from a teacher
	Then the user has a pass
	And the pass is paid
	And the pass is valid

@pass @create
Scenario: User purchases unlimited pass from a teacher
	Given a user exists
	And an unlimited pass template exists
	When the user purchases a pass from a teacher
	Then the user has a pass
	And the pass is paid
	And the pass is valid