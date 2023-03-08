import { FieldFetchableSelect, component, html, ifDefined, property } from '@3mo/model'
import { Task, TaskService } from 'sdk'

@component('sc-field-select-task')
export class FieldSelectTask extends FieldFetchableSelect<Task> {
	@property() override label = ''
	override readonly fetch = TaskService.getAll
	override readonly optionTemplate = (task: Task) => html`
		<mo-option value=${ifDefined(task.id)} .data=${task}>
			${task.title}
		</mo-option>
	`
}

declare global {
	interface HTMLElementTagNameMap {
		'sc-field-select-task': FieldSelectTask
	}
}