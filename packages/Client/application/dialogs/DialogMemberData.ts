import { html, component, DialogComponent, css } from '@3mo/model'
import { Member } from 'sdk'

@component('sc-dialog-member-data')
export class DialogMemberData extends DialogComponent<{ member: Member }> {
	get member() { return this.parameters.member }

	static get styles() {
		return css`
			div {
				display: flex;
				flex-direction: column;
			}
			#divImage {
				display: flex;
				align-items: center;
				justify-content: center;
			}
			img {
				border-radius: 50%;
				background-color: darkgray;
				width: 150px;
				height: 150px;
				margin: 0 15px;
				background-size: cover;
			}
			table {
				width: 100%;
			}
			table tr {
				width: 100%;
			}
			table tr td {
				padding: 20px;
			}
			table tr td:nth-child(1) {
				width: 50%;
				color: darkgray;
				text-align: right;
			}
			table tr td:nth-child(2) {
				width: 50%;
				flex: 1;
				font-size: 18px;
				text-align: left;
			}
		`
	}

	protected get template() {
		return html`
			<mo-dialog heading='Details' primaryButtonText=''>
				<mo-flex alignItems='center'>
					<sc-thumbnail fileId=${this.member.imageId}></sc-thumbnail>
					<table>
						<tr>
							<td>Aufgaben</td>
							<td>${this.member.tasks?.map(memberTask => memberTask.task.title).join(' - ')}</td>
						</tr>

						${this.member.data.map(memberData => html`
							<tr>
								<td>${memberData.title}</td>
								<td>${memberData.response}</td>
							</tr>
						`)}
					</table>
				</mo-flex>
			</mo-dialog>
		`
	}
}