// import './ArticleDetail.css';
import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import Axios from "axios";
import { Item } from "semantic-ui-react";
import Loading from "./Loading";

const ComicDetail = props => {
  const [episodes, setEpisodes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [noContent, setNoContent] = useState("");
  useEffect(() => {
    const fetchEpisodes = async () => {
      try {
        const res = await Axios.get(`/values/${props.match.params.titleNo}`);
        setEpisodes(res.data);
        setLoading(false);
      } catch {
        setNoContent("Coming soon");
        setLoading(false);
      }
    };
    fetchEpisodes();
  }, []);
  if (loading) {
    return <Loading />;
  }
  if (noContent) {
    return <div>{noContent}</div>;
  }
  // if (episodes.length === 0) {
  //   return <div>Coming soon</div>;
  // }
  if (episodes.length > 0) {
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
              <Item.Image size="tiny" src={`/comic${ep.episodeThumbnail}`} />
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
  }
};

export default ComicDetail;
