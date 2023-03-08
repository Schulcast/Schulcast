import { FieldFetchableSelect, component, html, ifDefined, property } from '@3mo/model'
import { Member, MemberService } from 'sdk'

@component('sc-field-select-member')
export class FieldSelectMember extends FieldFetchableSelect<Member> {
	@property() override label = ''
	override readonly fetch = MemberService.getAll
	override readonly optionTemplate = (member: Member) => html`
		<mo-option value=${ifDefined(member.id)} .data=${member}>
			${member.nickname}
		</mo-option>
	`
}

declare global {
	interface HTMLElementTagNameMap {
		'sc-field-select-member': FieldSelectMember
	}
}