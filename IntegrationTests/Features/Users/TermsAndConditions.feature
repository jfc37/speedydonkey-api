Feature: TermsAndConditions

@user @terms_and_conditions
Scenario: User agrees to terms and conditions
	When the user agrees to the terms and conditions
	Then the request is successful
	And the users term and conditions agreement is recorded