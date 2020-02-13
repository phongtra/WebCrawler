import React from "react";
import { BrowserRouter, Switch, Route, Link } from "react-router-dom";

import ComicList from "../layout/ComicList";
import ComicDetail from "../layout/ComicDetail";
import EpisodeDetail from "../layout/EpisodeDetail";
import { Header, Container } from "semantic-ui-react";

const App = () => {
  return (
    <Container>
      <BrowserRouter>
        <Header>
          <Link style={{ color: "black" }} to="/">
            Webtoon Clone
          </Link>
        </Header>
        <Switch>
          <Route path="/" exact component={ComicList} />
          <Route path="/:titleNo" exact component={ComicDetail} />
          <Route path="/:titleNo/:hash" exact component={EpisodeDetail} />
        </Switch>
      </BrowserRouter>
    </Container>
  );
};

export default App;
