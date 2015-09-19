Feature: PurchasePass

@pass @create
Scenario: User purchases pass from a teacher
	Given a user exists
	And a pass template exists
	When the user purchases a pass from a teacher
	Then the user has a pass
	And the pass is paid
	And the pass is valid