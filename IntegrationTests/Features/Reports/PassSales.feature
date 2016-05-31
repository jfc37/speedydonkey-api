Feature: PassSales

@pass_sales @reports
Scenario: Generate pass sales report - single pass
	Given '1' type of pass costing '20.00' has been sold
	When the pass sales report is requested
	Then the request is successful
	And the pass sales report has '1' line
	And the pass sales report total sold is '1'
	And the pass sales report toal revenue is '20.00'

@pass_sales @reports
Scenario: Generate pass sales report - multiple passes of same type
	Given '2' type of pass costing '20.00' has been sold
	When the pass sales report is requested
	Then the request is successful
	And the pass sales report has '1' line
	And the pass sales report total sold is '2'
	And the pass sales report toal revenue is '40.00'

@pass_sales @reports
Scenario: Generate pass sales report - multiple passes of different types
	Given '1' type of pass costing '20.00' has been sold
	Given '1' type of pass costing '20.00' has been sold
	When the pass sales report is requested
	Then the request is successful
	And the pass sales report has '2' line
	And the pass sales report total sold is '2'
	And the pass sales report toal revenue is '40.00'

@pass_sales @reports @validation_errors
Scenario: Failed generate pass sales report - no dates provided
	When the pass sales report is requested with no dates
	Then the request is unsuccessful

@pass_sales @reports @validation_errors
Scenario: Failed generate pass sales report - from date is after to date
	When the pass sales report is requested with from date being after to date
	Then the request is unsuccessful