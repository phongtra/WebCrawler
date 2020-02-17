import React from "react";
import { BrowserRouter, Switch, Route, Link } from "react-router-dom";

import ComicGenreSort from "../layout/comics/ComicGenreSort";
import ComicList from "../layout/comics/ComicList";
import ComicDetail from "../layout/comics/ComicDetail";
import EpisodeDetail from "../layout/comics/EpisodeDetail";

const App = () => {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/" exact component={ComicList} />
        <Route path="/:genre" exact component={ComicGenreSort} />
        <Route path="/:genre/:subject/:titleNo" exact component={ComicDetail} />
        <Route
          path="/:genre/:subject/:titleNo/:ep/:hash"
          exact
          component={EpisodeDetail}
        />
      </Switch>
    </BrowserRouter>
  );
};

export default App;
