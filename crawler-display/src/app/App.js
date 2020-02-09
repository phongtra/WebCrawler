import React from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';

import ArticleList from '../layout/ArticleList';
import ArticleDetail from '../layout/ArticleDetail';
import { Header } from 'semantic-ui-react';

const App = () => {
  return (
    <div>
      <Header>VNExpressClone</Header>
      <BrowserRouter>
        <Switch>
          <Route path="/" exact component={ArticleList} />
          <Route path="/:id" exact component={ArticleDetail} />
        </Switch>
      </BrowserRouter>
    </div>
  );
};

export default App;
