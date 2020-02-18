import React, { useState, useEffect, createRef } from "react";
import { Link } from "react-router-dom";
import Axios from "axios";
import Loading from "../commons/Loading";
import AdGrid from "../commons/AdGrid";
import { Menu, Dropdown, Sticky } from "semantic-ui-react";

const EpisodeDetail = props => {
  const [episodeContent, setEpisodeContent] = useState({});
  const [loading, setLoading] = useState(true);
  const [noContent, setNoContent] = useState("");
  const [episodes, setEpisodes] = useState([]);
  const contextRef = createRef();
  useEffect(() => {
    const fetchEpisodeContent = async () => {
      try {
        const [res1, res2] = await Promise.all([
          Axios.get(
            `/values/${props.match.params.titleNo}/${props.match.params.hash}`
          ),
          Axios.get(`/values/${props.match.params.titleNo}`)
        ]);
        // const res = await Axios.get(
        //   `/values/${props.match.params.titleNo}/${props.match.params.hash}`
        // );
        setEpisodeContent(res1.data);
        setEpisodes(res2.data);
        setLoading(false);
      } catch {
        setNoContent("Coming soon");
        setLoading(false);
      }
    };
    fetchEpisodeContent();
  }, []);

  const renderHeader = () => {
    return (
      <Menu inverted>
        <Link
          to={`/${props.match.params.genre}/${props.match.params.subject}/${props.match.params.titleNo}`}
          className="header item"
        >
          {props.match.params.subject}
        </Link>
        <Menu.Item header>Episode {props.match.params.ep}</Menu.Item>

        {episodes.length > 0 && (
          <Dropdown item text="Episodes">
            <Dropdown.Menu>
              {episodes.map((ep, i) => {
                return (
                  <a
                    href={`/${props.match.params.genre}/${
                      props.match.params.subject
                    }/${props.match.params.titleNo}/${ep.episodeName.match(
                      /\d+/g
                    )}/${ep.episodeLinkHash}`}
                    role="option"
                    className="item"
                    key={i}
                  >
                    Episode {ep.episodeName.match(/\d+/g)}
                  </a>
                );
              })}
            </Dropdown.Menu>
          </Dropdown>
        )}
      </Menu>
    );
  };
  if (loading) {
    return <Loading />;
  }
  if (noContent) {
    return (
      <>
        {renderHeader()}
        <AdGrid>{noContent}</AdGrid>
      </>
    );
  }
  if (episodeContent.content) {
    return (
      <div ref={contextRef}>
        <Sticky context={contextRef}>{renderHeader()}</Sticky>
        <AdGrid>
          <div>
            {JSON.parse(episodeContent.content).map((img, i) => {
              return (
                <img
                  style={{ verticalAlign: "top" }}
                  key={i}
                  height="1000"
                  width="800"
                  src={`/comic${img}`}
                />
              );
            })}
          </div>
        </AdGrid>
      </div>
    );
  }
};

export default EpisodeDetail;
