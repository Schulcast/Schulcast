import { Entity, Member, model } from 'sdk'

@model('MemberData')
export class MemberData extends Entity {
	title?: string
	response?: string
	memberId?: number
	member?: Member
}