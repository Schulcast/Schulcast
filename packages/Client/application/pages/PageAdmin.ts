import { component, html, state, PageComponent, property, route, nothing, style, css } from '@3mo/model'
import { API, Slide, Task, Post, Member } from 'sdk'
import { DialogAuthenticate, DialogPost, DialogMember, DialogSlide, DialogTask } from '../dialogs'

const enum Tab {
	Slides = 'slides',
	Tasks = 'tasks',
	Members = 'members',
	Posts = 'posts',
}

@route('/admin')
@component('sc-page-admin')
export class PageAdmin extends PageComponent {
	@property({ type: Object }) slides = new Array<Slide>()
	@property({ type: Object }) tasks = new Array<Task>()
	@property({ type: Object }) posts = new Array<Post>()
	@property({ type: Object }) members = new Array<Member>()

	@state() private tab = Tab.Posts

	protected initialized() {
		this.fetchSlides()
		this.fetchTasks()
		this.fetchMembers()
		this.fetchPosts()
	}

	private readonly fetchSlides = async () => this.slides = await API.get('slide') ?? []
	private readonly fetchTasks = async () => this.tasks = await API.get('task') ?? []
	private readonly fetchMembers = async () => this.members = await API.get('member') ?? []
	private readonly fetchPosts = async () => this.posts = await API.get('blog') ?? []

	private readonly authenticate = async () => {
		await new DialogAuthenticate().confirm()
		this.requestUpdate()
		await this.updateComplete
	}

	private unauthenticate() {
		DialogAuthenticate.unauthenticate()
		new PageAdmin().navigate()
	}

	static get styles() {
		return css`
			h2 {
				text-align: center;
			}

			mo-card {
				font-size: var(--mo-font-size-xl);
			}
		`
	}

	protected get template() {
		return html`
			<mo-page heading='Admin Center' fullHeight>
				<mo-tab-bar slot='headingDetails' value=${this.tab} @change=${(e: CustomEvent<Tab>) => this.tab = e.detail}>
					<mo-tab value=${Tab.Posts} label='Blogeinträge'></mo-tab>
					<mo-tab value=${Tab.Members} label='Mitglieder'></mo-tab>
					${DialogAuthenticate.authenticatedMember?.role !== 'Admin' ? nothing : html`
						<mo-tab value=${Tab.Slides} label='Slideshow'></mo-tab>
						<mo-tab value=${Tab.Tasks} label='Aufgabengruppen'></mo-tab>
					`}
				</mo-tab-bar>

				<mo-flex>
					${!DialogAuthenticate.isAuthenticated ? nothing : html`
						<mo-grid columns='repeat(auto-fit, minmax(225px, 1fr))' gap='var(--mo-thickness-m)' ${style({ width: '100%' })}>
							${this.currentTabCards}
						</mo-grid>
					`}

					${!DialogAuthenticate.isAuthenticated ? nothing : html`
						<mo-fab icon='add'
							${style({ position: 'absolute', right: '16px', bottom: '85px' })}
							@click=${this.openDialog}
						>${this.currentTabAction}</mo-fab>
					`}

					<mo-fab
						${style({ position: 'absolute', right: '16px', bottom: '16px' })}
						icon=${DialogAuthenticate.isAuthenticated ? 'logout' : 'login'}
						@click=${DialogAuthenticate.isAuthenticated ? this.unauthenticate : this.authenticate}
					>${DialogAuthenticate.isAuthenticated ? 'Ausloggen' : 'Einloggen'}</mo-fab>
				</mo-flex>
			</mo-page>
		`
	}

	private get currentTabCards() {
		switch (this.tab) {
			case Tab.Slides:
				return this.slides.map(slide => html`<mo-card @click=${() => this.openSlideDialog(slide)}>${slide.description}</mo-card>`)
			case Tab.Tasks:
				return this.tasks.map(task => html`<mo-card @click=${() => this.openTaskDialog(task)}>${task.title}</mo-card>`)
			case Tab.Members:
				return this.members.map(member => html`<mo-card @click=${() => this.openMemberDialog(member)}>${member.nickname}</mo-card>`)
			case Tab.Posts:
				return this.posts.map(post => html`<mo-card @click=${() => this.openPostDialog(post)}>${post.title}</mo-card>`)
		}
	}

	private get currentTabAction() {
		switch (this.tab) {
			case Tab.Slides:
				return 'Neue Slide'
			case Tab.Tasks:
				return 'Neue Aufgabengruppe'
			case Tab.Members:
				return 'Neuer Mitglied'
			case Tab.Posts:
				return 'Neuer Blogeintrag'
		}
	}

	private get openDialog() {
		switch (this.tab) {
			case Tab.Slides:
				return () => this.openSlideDialog()
			case Tab.Tasks:
				return () => this.openTaskDialog()
			case Tab.Members:
				return () => this.openMemberDialog()
			case Tab.Posts:
				return () => this.openPostDialog()
		}
	}

	private readonly openSlideDialog = async (slide?: Slide) => {
		await new DialogSlide({ entity: slide }).confirm()
		await this.fetchSlides()
	}

	private readonly openTaskDialog = async (task?: Task) => {
		await new DialogTask({ entity: task }).confirm()
		await this.fetchTasks()
	}

	private readonly openMemberDialog = async (member?: Member) => {
		await new DialogMember({ entity: member }).confirm()
		await this.fetchMembers()
	}

	private readonly openPostDialog = async (post?: Post) => {
		await new DialogPost({ entity: post }).confirm()
		await this.fetchPosts()
	}
}