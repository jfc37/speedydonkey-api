Feature: BlockEnrolment

@block @enrolment
Scenario: User enrols in a block
Given a block exists
When the user enrols in the block
Then the user is enroled in the block
And the user has an item in their upcoming schedule