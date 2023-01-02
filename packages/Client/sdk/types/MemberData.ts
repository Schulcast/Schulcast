import { Entity, Member } from '.'

export interface MemberData extends Entity {
	title: string
	response: string
	memberId: number
	member: Member
}