import { component, html } from '@3mo/model'
import { entityDialogComponent, EntityDialogComponent } from './EntityDialogComponent'
import { Task } from 'sdk'

@entityDialogComponent('task')
@component('sc-dialog-task')
export class DialogTask extends EntityDialogComponent<Task> {
	private get header() {
		return this.entity.id
			? `Aufgabe #${this.entity.id}`
			: 'Neue Aufgabe'
	}

	protected get template() {
		return html`
			<mo-dialog heading=${this.header}>
				<mo-field-text label='Titel' required
					value=${this.entity.title}
					@change=${(e: CustomEvent<string>) => this.entity.title = e.detail}
				></mo-field-text>
			</mo-dialog>
		`
	}
}