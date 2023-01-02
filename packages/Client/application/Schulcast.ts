import { component, css, html, property, Localizer, ThemeHelper, Color, ApplicationLogo, routerLink, Application, BusinessSuiteApplication } from '@3mo/model'
import * as Pages from './pages'


ThemeHelper.accent.value = new Color([204, 14, 0])
Localizer.currentLanguage = LanguageCode.German
ApplicationLogo.source = '/assets/logo.png'

@component('sc-application')
export class Schulcast extends Application {
	static get styles() {
		return css`
			${BusinessSuiteApplication.styles}

			${super.styles}

			mo-icon-button[icon=menu] {
				display: none;
			}

			#flexPages a {
				display: flex;
				color: var(--mo-color-accessible);
				font-size: var(--mo-font-size-m);
			}

			[application] {
				--footer-height: 40px;
				--player-height: 0px;
			}

			[application][playing] {
				--player-height: 80px;
			}

			mo-top-app-bar {
				padding-bottom: calc(var(--footer-height) + var(--player-height));
			}

			sc-player {
				bottom: var(--footer-height);
			}

			lit-page-host {
				padding-bottom: 40px;
			}

			@media (max-width: 768px) {
				mo-icon-button[icon=menu] {
					display: block;
				}

				#flexPages {
					display: none;
				}

				[application] {
					--footer-height: 0px;
				}

				sc-footer {
					display: none;
				}
			}
		`
	}

	@property({ type: Boolean, reflect: true }) playing = false

	protected get bodyTemplate() {
		return html`
			${super.bodyTemplate}
			<sc-player @sourceChange=${(e: CustomEvent<string | undefined>) => this.playing = !!e.detail}></sc-player>
			<sc-footer></sc-footer>
		`
	}

	get drawerTemplate() {
		return html`
			<mo-list>
				<mo-navigation-list-item icon='home' ${routerLink(new Pages.PageHome)}>Startseite</mo-navigation-list-item>
				<mo-navigation-list-item icon='group' ${routerLink(new Pages.PageTeam)}>Team</mo-navigation-list-item>
				<mo-navigation-list-item icon='info' ${routerLink(new Pages.PageImprint)}>Impressum</mo-navigation-list-item>
			</mo-list>
		`
	}

	get drawerFooterTemplate() {
		return html`
			<mo-flex gap='20px'>
				<sc-social-media></sc-social-media>
				<sc-copyright></sc-copyright>
			</mo-flex>
		`
	}

	get topAppBarActionItemsTemplate() {
		return html`
			<mo-flex id='flexPages' direction='horizontal' gap='var(--mo-thickness-xl)' alignItems='center'>
				<a href='' @click=${() => new Pages.PageHome().navigate()}>Startseite</a>
				<a href='' @click=${() => new Pages.PageTeam().navigate()}>Team</a>
			</mo-flex>
		`
	}
}