Feature: InviteOnlyBlock

@block @block_invite_only @create @golden_path
Scenario: Create invite only block
	Given a valid block is ready to be submitted
	And the block is invite only
	When the block is attempted to be created
	Then block can be retrieved
	And the block is invite only

@block @block_invite_only @create @golden_path
Scenario: Invite only block not available for enrolment
	Given an invite only block exists
	When blocks for enrolment is requested
	Then the request returns not found

@block @block_invite_only @create @golden_path
Scenario: Generate invite only second block
	Given an invite only block exists
	When the next block is generated
	Then block can be retrieved
	And the block is invite only