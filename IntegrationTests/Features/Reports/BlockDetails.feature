Feature: BlockDetails

@block_details @reports
Scenario: Generate block details report - clip passes - single class, single student
	Given a block with '4' classes exists
	And '1' student has '1' class pass costing '20.00'
	And '1' student attends '1' class of '1' block
	When the block details report is requested
	Then the request is successful
	And the block details report has '4' line
	And the block details total attendance is '1'
	And the block details total revenue is '20.00'
	And line '1' of block details report revenue is '20.00'
	And line '1' of block details report attendance is '1'
	And line '2' of block details report revenue is '0.00'
	And line '2' of block details report attendance is '0'

@block_details @reports
Scenario: Generate block details report - clip passes - multiple classes, single student
	Given a block with '4' classes exists
	And '1' student has '2' class pass costing '20.00'
	And '1' student attends '2' class of '1' block
	When the block details report is requested
	Then the request is successful
	And the block details report has '4' line
	And the block details total attendance is '2'
	And the block details total revenue is '20.00'
	And line '1' of block details report revenue is '10.00'
	And line '1' of block details report attendance is '1'
	And line '2' of block details report revenue is '10.00'
	And line '2' of block details report attendance is '1'
	And line '3' of block details report revenue is '0.00'
	And line '3' of block details report attendance is '0'

@block_details @reports
Scenario: Generate block details report - clip passes - multiple classes, multiple students
	Given a block with '4' classes exists
	And '2' student has '2' class pass costing '20.00'
	And '2' student attends '2' class of '1' block
	When the block details report is requested
	Then the request is successful
	And the block details report has '4' line
	And the block details total attendance is '4'
	And the block details total revenue is '40.00'
	And line '1' of block details report revenue is '20.00'
	And line '1' of block details report attendance is '2'
	And line '2' of block details report revenue is '20.00'
	And line '2' of block details report attendance is '2'
	And line '3' of block details report revenue is '0.00'
	And line '3' of block details report attendance is '0'

@block_details @reports
Scenario: Generate block details report - unlimited passes - multiple classes, multiple students
	Given a block with '6' classes exists
	And '2' student has a '4' week unlimited pass costing '20.00'
	And '2' student attends '2' class of '1' block
	When the block details report is requested
	Then the request is successful
	And the block details report has '6' line
	And the block details total attendance is '4'
	And the block details total revenue is '20.00'
	And line '1' of block details report revenue is '10.00'
	And line '1' of block details report attendance is '2'
	And line '2' of block details report revenue is '10.00'
	And line '2' of block details report attendance is '2'
	And line '3' of block details report revenue is '0.00'
	And line '3' of block details report attendance is '0'
