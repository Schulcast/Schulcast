import { Entity, IsoDate, Member } from '.'

export interface Post extends Entity {
	title: string
	content: string
	published: IsoDate
	lastUpdated: IsoDate
	memberId: number
	member?: Member
}