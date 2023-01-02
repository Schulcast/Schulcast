import { component, html, Component, property, ifDefined, css } from '@3mo/model'
import { Member as MemberType } from 'sdk'

@component('sc-member')
export class Member extends Component {
	@property({ type: Object }) member?: MemberType

	static get styles() {
		return css`
			div {
				display: flex;
				flex-direction: column;
				width: 180px;
				height: 240px;
				margin: 10px;
				transition-duration: 600ms;
				cursor: pointer;

				padding: 5px;
			}

			div:hover {
				background-color: var(--mo-color-background);
			}

			h3 {
				text-align: center;
				font-size: 1.25em;
				margin: 0;
				line-height: 50px;
			}

			p {
				color: rgb(128,128,128);
				text-align: center;
				flex: 1;
				line-height: 20px;
				margin: 0;
			}
		`
	}

	protected get template() {
		return html`
			<style>
				:host {
					display: ${this.member?.role === 'Member' ? 'inline-block' : 'none'};
				}
			</style>
			<div>
				<sc-thumbnail fileId=${ifDefined(this.member?.imageId)}></sc-thumbnail>
				<h3>${this.member?.nickname}</h3>
				<p>${this.member?.tasks?.map(task => task.task.title).join('-')}</p>
			</div>
		`
	}
}

declare global {
	interface HTMLElementTagNameMap {
		'sc-member': Member
	}
}