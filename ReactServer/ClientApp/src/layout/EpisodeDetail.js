import React, { useState, useEffect } from "react";
import Axios from "axios";
import Loading from "./Loading";

const EpisodeDetail = props => {
  const [episodeContent, setEpisodeContent] = useState({});
  const [loading, setLoading] = useState(true);
  const [noContent, setNoContent] = useState("");

  useEffect(() => {
    const fetchEpisodeContent = async () => {
      try {
        const res = await Axios.get(
          `/values/${props.match.params.titleNo}/${props.match.params.hash}`
        );
        setEpisodeContent(res.data);
        setLoading(false);
      } catch {
        setNoContent("Coming soon");
        setLoading(false);
      }
    };
    fetchEpisodeContent();
  }, []);
  if (loading) {
    return <Loading />;
  }
  if (noContent) {
    return <div>{noContent}</div>;
  }
  if (episodeContent.content) {
    return (
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
    );
  }
};

export default EpisodeDetail;
