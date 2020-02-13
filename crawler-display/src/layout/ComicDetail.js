// import './ArticleDetail.css';
import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import Axios from "axios";
import { Item } from "semantic-ui-react";

const ComicDetail = props => {
  const [episodes, setEpisodes] = useState([]);

  useEffect(() => {
    const fetchEpisodes = async () => {
      const res = await Axios.get(`/values/${props.match.params.titleNo}`);
      setEpisodes(res.data);
    };
    fetchEpisodes();
  }, []);

  if (episodes.length === 0) return <div>Coming soon</div>;
  return (
    <Item.Group link divided unstackable>
      {episodes.map((ep, i) => {
        return (
          <Link
            to={`/${ep.titleNo}/${ep.episodeLinkHash}`}
            class="item"
            style={{ color: "black" }}
            key={i}
          >
            <Item.Image size="tiny" src={ep.episodeThumbnail} />
            <Item.Content verticalAlign="middle">
              <Item.Header
                dangerouslySetInnerHTML={{ __html: ep.episodeName }}
              />
            </Item.Content>
            <Item.Content verticalAlign="bottom">
              <br />
              <br />
              <br />
              <Item.Description verticalAlign="bottom">
                {ep.episodeDate}
              </Item.Description>
            </Item.Content>
          </Link>
        );
      })}
    </Item.Group>
  );
};

export default ComicDetail;
