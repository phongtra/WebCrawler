import React from 'react';
import { BrowserRouter, Switch, Route, Link } from 'react-router-dom';

import ComicList from '../layout/ComicList';
import ComicDetail from '../layout/ComicDetail';
import EpisodeDetail from '../layout/EpisodeDetail';
import { Header, Grid, Responsive, Segment } from 'semantic-ui-react';

const App = () => {
  return (
    <BrowserRouter>
      <Header>
        <Link style={{ color: 'black' }} to="/">
          Webtoon Clone
        </Link>
      </Header>
      <Grid container columns={3}>
        <Grid.Column width={1}>
          <Responsive minWidth={1024}>
            <Segment></Segment>
          </Responsive>
        </Grid.Column>
        <Grid.Column width={14}>
          <Responsive maxWidth={500}>
            <Segment></Segment>
          </Responsive>
          <Switch>
            <Route path="/" exact component={ComicList} />
            <Route path="/:titleNo" exact component={ComicDetail} />
            <Route path="/:titleNo/:hash" exact component={EpisodeDetail} />
          </Switch>
        </Grid.Column>
        <Grid.Column width={1}>
          <Responsive minWidth={1024}>
            <Segment></Segment>
          </Responsive>
        </Grid.Column>
      </Grid>
    </BrowserRouter>
  );
};

export default App;
