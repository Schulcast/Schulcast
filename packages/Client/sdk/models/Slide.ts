import { model, Entity } from 'sdk'

@model('Slide')
export class Slide extends Entity {
	description?: string
	imageId?: number
}