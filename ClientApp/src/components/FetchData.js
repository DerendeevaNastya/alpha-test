import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { productsTop: [], productsActual: [], productsPopular: [], loading: false };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(productsTop) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id товара</th>
            <th>Наименование товара</th>
            <th>Цена</th>
            <th>Количество покупок</th>
          </tr>
        </thead>
        <tbody>
          {productsTop.map(product =>
            <tr key={product.id}>
              <td>{product.id}</td>
              <td>{product.name}</td>
              <td>{product.price}</td>
              <td>{product.count}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.productsTop);

    return (
      <div>
        <h1 id="tabelLabel" >Продукты</h1>
        <p>Здесь показаны топ 5 популярных продуктов, по url products + /top, /popular, /actual можно посмотреть результат выполнения процедуры</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const responseTopProducts = await fetch('products/top');
    const responsePopProducts = await fetch('products/popular');
    const responseActProducts = await fetch('products/actual');
    const top = await responseTopProducts.json();
    const act = await responseActProducts.json();
    const pop = await responsePopProducts.json();
    this.setState({ productsTop: top, productsActual: act, productsPopular: pop, loading: false });
  }
}
