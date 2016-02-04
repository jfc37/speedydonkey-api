Feature: DoNotEmailUser
	
@user @do_not_email
Scenario: Set do not email flag on user works
	Given a block exists
	And the user enrols in the block
	And the user does not want to receive emails
	When an announcement is sent to the block
	Then an email was sent to '0' users

@user @do_not_email
Scenario: Remove do not email flag on user works
	Given a block exists
	And the user enrols in the block
	And the user does not want to receive emails
	And the user does want to receive emails
	When an announcement is sent to the block
	Then an email was sent to '1' users
