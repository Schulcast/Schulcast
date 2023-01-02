import { component, PageComponent, route, html, property, css, style } from '@3mo/model'
import { API, Post } from 'sdk'
import { Marked } from '@ts-stack/markdown'

@route('/blog/:id')
@component('sc-page-blog')
export class PageBlog extends PageComponent<{ id: number }> {
	@property({ type: Object }) post?: Post

	protected async initialized() {
		this.post = await API.get(`blog/${this.parameters.id}`)
	}

	static get styles() {
		return css`
			:host {
				font-size: 18px;
				line-height: 30px;
			}

			mo-card {
				--mo-card-padding-vertical: 16px;
				--mo-card-padding-horizontal: 16px;
			}

			h1 {
				overflow-wrap: break-word;
				color: var(--mo-color-accent);
				flex: 1;
				margin: 16px 0 0 0;
				font-weight: 500;
			}

			div * {
				max-width: 100%;
			}
		`
	}

	protected get template() {
		return html`
			<mo-page heading='Blogeintrag'>
				<mo-card>
					<mo-flex ${style({ color: 'var(--mo-color-gray)' })}>
						<h1>${this.post?.title}</h1>
						<span>${this.post?.member?.nickname} ● ${this.post?.published ? new MoDate(this.post.published).toLocaleDateString('de', { year: 'numeric', month: 'long', day: 'numeric' }) : ''}</span>
					</mo-flex>
					<div .innerHTML=${this.post?.content ? Marked.parse(this.post.content) : ''}></div>
				</mo-card>
			</mo-page>
		`
	}
}