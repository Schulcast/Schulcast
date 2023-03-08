import { Api, Slide } from 'sdk'

export class SlideService {
	static get(id: number) {
		return Api.get<Slide>(`/slide/${id}`)
	}

	static getAll() {
		return Api.get<Array<Slide>>(`/slide`)
	}

	static save(slide: Slide) {
		return !slide.id
			? Api.post<Slide>('/slide', slide)
			: Api.put<Slide>(`/slide`, slide)
	}

	static delete(slide: Slide) {
		return Api.delete(`/slide/${slide.id}`)
	}
}