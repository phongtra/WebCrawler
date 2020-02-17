// import './ArticleDetail.css';
import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import Axios from "axios";
import { Item } from "semantic-ui-react";
import AdGrid from "../commons/AdGrid";
import Loading from "../commons/Loading";
import NavBar from "../nav/NavBar";

const ComicDetail = props => {
  const [episodes, setEpisodes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [noContent, setNoContent] = useState("");
  const [filter, setFilter] = useState([]);
  useEffect(() => {
    const fetchEpisodes = async () => {
      try {
        const res = await Axios.get(`/values/${props.match.params.titleNo}`);
        setEpisodes(res.data);
        setFilter(res.data);
        setLoading(false);
      } catch {
        setNoContent("Coming soon");
        setLoading(false);
      }
    };
    fetchEpisodes();
  }, []);
  const handleFilter = filter => {
    setFilter(
      [...episodes].filter(
        ep =>
          ep.episodeName
            .match(/\d+/g)[0]
            .toLowerCase()
            .indexOf(filter.toLowerCase()) > -1
      )
    );
  };
  if (loading) {
    return <Loading />;
  }
  if (noContent) {
    return (
      <>
        <NavBar />
        <AdGrid>
          <div>{noContent}</div>
        </AdGrid>
      </>
    );
  }
  // if (episodes.length === 0) {
  //   return <div>Coming soon</div>;
  // }
  if (episodes.length > 0) {
    return (
      <>
        <NavBar
          handleFilter={handleFilter}
          placeholder={`Search from ${props.match.params.subject}`}
        />
        <AdGrid>
          <Item.Group link divided unstackable>
            {filter.map((ep, i) => {
              return (
                <Link
                  to={`/${props.match.params.genre}/${
                    props.match.params.subject
                  }/${ep.titleNo}/${ep.episodeName.match(/\d+/g)}/${
                    ep.episodeLinkHash
                  }`}
                  className="item"
                  style={{ color: "black" }}
                  key={i}
                >
                  <Item.Image
                    size="tiny"
                    src={`/comic${ep.episodeThumbnail}`}
                  />
                  <Item.Content verticalAlign="middle">
                    <Item.Header>
                      Episode {ep.episodeName.match(/\d+/g)}
                    </Item.Header>
                  </Item.Content>
                  <Item.Content verticalAlign="bottom">
                    <br />
                    <br />
                    <br />
                    <Item.Description>{ep.episodeDate}</Item.Description>
                  </Item.Content>
                </Link>
              );
            })}
          </Item.Group>
        </AdGrid>
      </>
    );
  }
};

export default ComicDetail;
