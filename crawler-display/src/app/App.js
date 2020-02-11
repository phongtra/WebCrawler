import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";

import ArticleList from "../layout/ArticleList";
import ArticleDetail from "../layout/ArticleDetail";
import { Header } from "semantic-ui-react";
const images = `<img src="https://webtoons-static.pstatic.net/image/bg_transparency.png" width="800" height="1280.0" alt="image" class="_images" data-url="https://webtoon-phinf.pstatic.net/20200106_123/1578288340418m5rdG_JPEG/15782883403816791764.jpg?type=q90" rel="nofollow" ondragstart="return false;" onselectstart="return false;" oncontextmenu="return false;">`;
const App = () => {
  return (
    <div>
      <img
        src="https://webtoon-phinf.pstatic.net/20200113_50/1578880362591ha4TW_JPEG/15788803625521468598.jpg?type=q90\"
        width="800"
        height="1280.0"
      />
    </div>
  );

  {
    /* <Header>VNExpressClone</Header>
      <BrowserRouter>
        <Switch>
          <Route path="/" exact component={ArticleList} />
          <Route path="/:id" exact component={ArticleDetail} />
        </Switch>
      </BrowserRouter> */
  }
  {
    /* </div>
  ); */
  }
};

export default App;
