Feature: BlockSummary

@block_summary @reports
Scenario: Generate block summary report - clip passes - single block, single class, single student
	Given '1' blocks exists
	And '1' student has '1' class pass costing '20.00'
	And '1' student attends '1' class of '1' block
	When the block summary report is requested
	Then the request is successful
	And the block summary report has '1' line
	And the block summary total attendance is '1'
	And the block summary total revenue is '20.00'

@block_summary @reports
Scenario: Generate block summary report - clip passes - single block, single class, multiple students
	Given '1' blocks exists
	And '2' student has '1' class pass costing '20.00'
	And '2' student attends '1' class of '1' block
	When the block summary report is requested
	Then the request is successful
	And the block summary report has '1' line
	And the block summary total attendance is '2'
	And the block summary total revenue is '40.00'

@block_summary @reports
Scenario: Generate block summary report - clip passes - single block, multiple class, multiple students
	Given '1' blocks exists
	And '2' student has '2' class pass costing '20.00'
	And '2' student attends '2' class of '1' block
	When the block summary report is requested
	Then the request is successful
	And the block summary report has '1' line
	And the block summary total attendance is '4'
	And the block summary total revenue is '40.00'

@block_summary @reports
Scenario: Generate block summary report - clip passes - multiple block, multiple class, multiple students
	Given '2' blocks exists
	And '2' student has '4' class pass costing '20.00'
	And '2' student attends '2' class of '2' block
	When the block summary report is requested
	Then the request is successful
	And the block summary report has '2' line
	And the block summary total attendance is '8'
	And the block summary total revenue is '40.00'

@block_summary @reports
Scenario: Generate block summary report - unlimited passes - single block, single class, single student
	Given '1' blocks with '4' classes exists
	And '1' student has a '4' week unlimited pass costing '20.00'
	And '1' student attends '1' class of '1' block
	When the block summary report is requested
	Then the request is successful
	And the block summary report has '1' line
	And the block summary total attendance is '1'
	And the block summary total revenue is '5.00'

@block_summary @reports
Scenario: Generate block summary report - unlimited passes - single block, single class, multiple students
	Given '1' blocks with '4' classes exists
	And '2' student has a '4' week unlimited pass costing '20.00'
	And '2' student attends '1' class of '1' block
	When the block summary report is requested
	Then the request is successful
	And the block summary report has '1' line
	And the block summary total attendance is '2'
	And the block summary total revenue is '10.00'

@block_summary @reports
Scenario: Generate block summary report - unlimited passes - single block, multiple class, multiple students
	Given '1' blocks with '4' classes exists
	And '2' student has a '4' week unlimited pass costing '20.00'
	And '2' student attends '2' class of '1' block
	When the block summary report is requested
	Then the request is successful
	And the block summary report has '1' line
	And the block summary total attendance is '4'
	And the block summary total revenue is '20.00'

@block_summary @reports
Scenario: Generate block summary report - unlimited passes - multiple block, multiple class, multiple students
	Given '2' blocks with '4' classes exists
	And '2' student has a '4' week unlimited pass costing '20.00'
	And '2' student attends '2' class of '2' block
	When the block summary report is requested
	Then the request is successful
	And the block summary report has '2' line
	And the block summary total attendance is '8'
	And the block summary total revenue is '20.00'

@block_summary @reports
Scenario: Generate block summary report - unlimited passes - pass doesnt cover all classes
	Given '1' blocks with '4' classes exists
	And '1' student has a '2' week unlimited pass costing '20.00'
	And '1' student attends '1' class of '1' block
	When the block summary report is requested
	Then the request is successful
	And the block summary report has '1' line
	And the block summary total attendance is '1'
	And the block summary total revenue is '10.00'
