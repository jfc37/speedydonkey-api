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

@pass @create
Scenario: User purchases multiple unlimited passes from a teacher
	Given a user exists
	And an unlimited pass template exists
	When the user purchases '2' passes from a teacher
	Then all passes expire on the same day

@pass @create
Scenario: User purchases multiple clip passes from a teacher
	Given a user exists
	And a pass template exists
	When the user purchases '2' passes from a teacher
	Then all passes expire on the same day

@pass @create @validation_errors
Scenario: User tries to purchase a pass that doesnt exist
	Given a user exists
	When the user purchases a pass that doesnt exist from a teacher
	Then validation errors are returned
	And the user doesnt have a pass

