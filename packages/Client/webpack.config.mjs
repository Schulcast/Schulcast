import { resolve } from 'path'
import CopyPlugin from 'copy-webpack-plugin'
import HtmlWebpackPlugin from 'html-webpack-plugin'
import TerserPlugin from 'terser-webpack-plugin'
import TsconfigPathsPlugin from 'tsconfig-paths-webpack-plugin'
import FaviconsWebpackPlugin from 'favicons-webpack-plugin'

export default (_, args) => {
	return {
		entry: './application/index.ts',
		context: resolve(''),
		output: {
			filename: 'main.[contenthash].js',
			path: resolve('dist'),
			publicPath: '/',
			filename: args.mode === 'development' ? 'main.js' : 'main.[contenthash].js'
		},
		watchOptions: {
			poll: 3000,
			ignored: /node_modules/
		},
		devtool: args.mode !== 'development' ? false : 'source-map',
		optimization: args.mode !== 'production' ? {
			minimize: false,
			minimizer: undefined
		} : {
			minimize: true,
			minimizer: [
				new TerserPlugin({
					terserOptions: {
						output: {
							comments: false,
						},
					},
					extractComments: false,
				})
			],
		},
		stats: 'minimal',
		module: {
			rules: [
				{
					test: /\.ts?$/,
					loader: 'ts-loader',
					options: { allowTsInNodeModules: true }
				}
			]
		},
		plugins: [
			new HtmlWebpackPlugin({
				templateContent: `
					<!DOCTYPE html>
					<html lang="en">
						<head>
							<meta charset="UTF-8">
							<meta http-equiv="X-UA-Compatible" content="IE=edge">
							<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=no">
							<title>Schulcast</title>
						</head>

						<body>
							<sc-application></sc-application>
						</body>
					</html>
				`
			}),
			new FaviconsWebpackPlugin({
				logo: 'assets/logo.png',
				manifest: './manifest.json',
				favicons: {
					appleStatusBarStyle: 'default'
				}
			}),
			new CopyPlugin({
				patterns: [
					{
						from: 'assets/',
						to: 'assets/'
					}
				]
			})
		],
		resolve: {
			extensions: ['.ts', '.js'],
			plugins: [
				new TsconfigPathsPlugin({ configFile: './tsconfig.json' })
			]
		}
	}
}