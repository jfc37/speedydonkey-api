Feature: RetrieveBlock

@block @retrieve @get_all
Scenario: Get all blocks
	Given a block exists
	When all blocks are retreived
	Then something is retreived

@block @retrieve @search
Scenario: Search blocks
	Given a block exists
	When a block search is performed
	Then something is retreived

@block @retrieve @get_by_id
Scenario: Get block by id
	Given a block exists
	When a block is retrieved by id
	Then something is retreived