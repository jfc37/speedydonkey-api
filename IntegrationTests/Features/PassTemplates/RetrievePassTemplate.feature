Feature: RetrievePassTemplate

@pass_template @retrieve @get_all
Scenario: Get all pass templates
	Given a pass template exists
	When all pass templates are retreived
	Then something is retreived

@pass_template @retrieve @search
Scenario: Search pass templates
	Given a pass template exists
	When a pass template search is performed
	Then something is retreived

@pass_template @retrieve @get_by_id
Scenario: Get pass template by id
	Given a pass template exists
	When a pass template is retrieved by id
	Then something is retreived