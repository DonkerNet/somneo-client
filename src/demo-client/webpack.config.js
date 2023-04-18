const path = require('path');

module.exports = {
  entry: './src/index.tsx',
  mode: 'development',
  devtool: 'source-map',
  module: {
    rules: [
      {
        test: /\.(ts|tsx)$/,
        exclude: /node_modules/,
        use: {
          loader: 'ts-loader'
        }
      },
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader'
        }
      }
    ]
  },
  resolve: {
    extensions: [
      '.ts', '.tsx',
      '.js', '.jsx',
    ]
  },
  output: {
    path: path.resolve(__dirname, '../Donker.Home.Somneo.DemoApp/wwwroot/js/build'),
    filename: 'demo-client.js'
  }
};
