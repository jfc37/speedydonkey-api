Feature: TeacherInvoices

@teacher_invoices @reports
Scenario: Generate teacher invoices report - solo teacher
	Given the default solo teacher rate is '10.00'
	And a block with '2' classes exists
	And the block has '1' teacher
	When the teacher invoice report is requested
	Then the request is successful
	And the teacher invoice report has '1' teacher
	And the teacher invoice report totals '20'

@teacher_invoices @reports
Scenario: Generate teacher invoices report - partnered teacher
Given an existing user is a teacher
	Given the default partnered teacher rate is '20.00'
	And a block with '2' classes exists
	And the block has '2' teacher
	When the teacher invoice report is requested
	Then the request is successful
	And the teacher invoice report has '2' teacher
	And the teacher invoice report totals '80'

@teacher_invoices @reports
Scenario: Generate teacher invoices report - solo and partnered teacher
	Given the default partnered teacher rate is '10.00'
	And the default solo teacher rate is '20.00'
	And a teacher is teaching a solo block and a partnered block
	When the teacher invoice report is requested
	Then the request is successful
	And the teacher invoice report has '2' teacher
	And the teacher invoice report totals '80'

@teacher_invoices @reports
Scenario: Generate teacher invoices report - event
	Given a stand alone event with '25' teacher rate exists
	When the teacher invoice report is requested
	Then the request is successful
	And the teacher invoice report has '1' teacher
	And the teacher invoice report totals '25'