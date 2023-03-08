import { Entity, Member, model } from 'sdk'

@model('Post')
export class Post extends Entity {
	title!: string
	tags?: string[]
	content!: string
	published!: Date
	lastUpdated!: Date
	memberId!: number
	member?: Member
}