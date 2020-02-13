import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";

import ComicList from "../layout/ComicList";
import ComicDetail from "../layout/ComicDetail";
import EpisodeDetail from "../layout/EpisodeDetail";
import { Header } from "semantic-ui-react";

const App = () => {
  return (
    <div>
      <Header>Webtoon Clone</Header>
      <BrowserRouter>
        <Switch>
          <Route path="/" exact component={ComicList} />
          <Route path="/:titleNo" exact component={ComicDetail} />
          <Route path="/:titleNo/:hash" exact component={EpisodeDetail} />
        </Switch>
      </BrowserRouter>
    </div>
  );
};

export default App;
